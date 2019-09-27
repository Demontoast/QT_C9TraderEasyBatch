import hashlib
import json
import logging
import os
import pathlib
import sys
import argparse
import browsepy
from gevent import pywsgi
from collections import OrderedDict
from flask import Flask, request
from werkzeug import exceptions
from werkzeug.utils import secure_filename
from threading import Thread

app = Flask(__name__)


@app.route('/api/<ptt>', methods=['POST'])
def record_request(ptt):
    if request.method == 'POST':
        app.logger.info(request)
        return process_call_record(request.files, request.form, ptt)


def process_call_record(files, form, ptt):
    call_record_file, call_record = files.get('callRecordFile'), form.get('callRecord')
    if call_record_file and call_record:
        file_bytes, json_dict = call_record_file.read(), json.loads(call_record, object_pairs_hook=OrderedDict)
        md5checksum = hashlib.md5(file_bytes).hexdigest()
        filename = os.path.join(app.config['directory'], 'uploads', ptt, secure_filename(call_record_file.filename))
        if md5checksum == json_dict['recordingFile']['md5Checksum']:
            try:
                os.makedirs(os.path.dirname(filename))
            except OSError:
                pass
            with open(str(pathlib.Path(filename).with_suffix('.json')), 'w') as json_out, \
                    open(filename, 'wb') as rec_out:
                rec_out.write(file_bytes)
                app.logger.info('Wrote file {}'.format(rec_out.name))
                json.dump(json_dict, json_out, indent=4)
                app.logger.info('Json dumped {}'.format(json_out.name))
                return app.make_response((md5checksum, 201))
        exceptions.abort(404)


def https_target(host, https_port, certfile, keyfile):
    server = pywsgi.WSGIServer((host, https_port), app, keyfile=keyfile, certfile=certfile)
    server.serve_forever()


def browser(host, port):
    server = pywsgi.WSGIServer((host, port), browsepy.app)
    server.serve_forever()


def get_args():
    parser = argparse.ArgumentParser()
    parser.add_argument('-host', dest='host', default='127.0.0.1', help='host address')
    parser.add_argument('-disable_ssl', dest='disable_ssl', action='store_true', help='Whether to disable ssl')
    parser.add_argument('-http_port', dest='http_port', type=int, default=8080, help='http port number')
    parser.add_argument('-https_port', dest='https_port', type=int, default=8443, help='https port number')
    parser.add_argument('-browse_port', dest='browse_port', type=int, default=80, help='browse port number')
    parser.add_argument('-directory', dest='directory', default=app.root_path, help='Where to store the collected files')
    parser.add_argument('-certfile', dest='certfile', default='c9localhost.crt', help='certificate file for ssl')
    parser.add_argument('-keyfile', dest='keyfile', default='nopasskey.pem', help='key file for ssl')
    return parser.parse_args()


def main():
    args = get_args()

    handler = logging.StreamHandler(sys.stdout)
    handler.setFormatter(logging.Formatter("%(asctime)s - %(name)s - %(levelname)s - %(message)s"))
    app.logger.addHandler(handler)
    app.logger.setLevel(logging.INFO)
    app.config.update(directory=args.directory)
    browsepy.app.config.update(APPLICATION_ROOT=os.path.join(args.directory, 'uploads'))
    browsepy.app.config.update(directory_start=os.path.join(args.directory, 'uploads'))
    if not args.disable_ssl:
        ssl_t = Thread(target=https_target, args=(args.host, args.https_port, args.certfile, args.keyfile))
        ssl_t.daemon = True
        ssl_t.start()
    browse_t = Thread(target=browser, args=(args.host, args.browse_port))
    browse_t.daemon = True
    browse_t.start()
    server = pywsgi.WSGIServer((args.host, args.http_port), app)
    server.serve_forever()


if __name__ == '__main__':
    main()
