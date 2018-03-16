import hashlib
import json
import logging
import os
import pathlib
import ssl
import sys
from collections import OrderedDict
from flask import Flask, request, url_for
from werkzeug import exceptions
from werkzeug.utils import secure_filename

app = Flask(__name__)


@app.route('/api/<ptt>', methods=['POST'])
def record_request(ptt):
    if request.method == 'POST':
        app.logger.info(request)
        return process_call_record(request.files, request.form, ptt)


@app.route('/')
def index():
    import urllib
    output = []
    for rule in app.url_map.iter_rules():

        options = {}
        for arg in rule.arguments:
            options[arg] = "[{0}]".format(arg)

        methods = ','.join(rule.methods)
        url = url_for(rule.endpoint, **options)
        line = urllib.unquote("{:50s} {:20s} {}".format(rule.endpoint, methods, url))
        output.append(line)

    return output


def process_call_record(files, form, ptt):
    call_record_file, call_record = files.get('callRecordFile'), form.get('callRecord')
    if call_record_file and call_record:
        file_bytes, json_dict = call_record_file.read(), json.loads(call_record, object_pairs_hook=OrderedDict)
        md5checksum = hashlib.md5(file_bytes).hexdigest()
        filename = os.path.join(app.root_path, 'uploads', ptt, secure_filename(call_record_file.filename))
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


def https_target():
    context = ssl.SSLContext(ssl.PROTOCOL_SSLv23)
    context.load_cert_chain('c9localhost.crt', 'nopasskey.pem')
    app.run(port=8443, threaded=True, debug=True, ssl_context=context)

if __name__ == '__main__':
    from multiprocessing import Process
    handler = logging.StreamHandler(sys.stdout)
    handler.setFormatter(logging.Formatter("%(asctime)s - %(name)s - %(levelname)s - %(message)s"))
    app.logger.addHandler(handler)
    app.logger.setLevel(logging.INFO)
    p = Process(target=https_target)
    p.daemon = True
    p.start()
    app.run(port=8080, threaded=True, debug=True)
