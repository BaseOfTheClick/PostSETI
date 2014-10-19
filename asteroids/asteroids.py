#!/usr/bin/env python3
from urllib.parse import urlparse
from urllib.request import Request, urlopen
import re

def download(url, filename):
    req = Request(url)
    reply = urlopen(req)
    with open(filename, 'wb') as fh:
        fh.write(reply.read())
    reply.close()

prefix = 'http://echo.jpl.nasa.gov/asteroids/shapes'

req = Request('{}/shapes.html'.format(prefix))
reply = urlopen(req)

buf = reply.read()
reply.close()

#print(buf)

regex = re.compile(r'"(.*\.obj)"')

for match in re.findall(regex, buf.decode('UTF-8')):
    print('Downloading {}/{}'.format(prefix, match))
    download('{}/{}'.format(prefix, match), match)
    print('Downloaded {}'.format(match))

