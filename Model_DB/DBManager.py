#db
# 1. db를 2개로 나눠서 관리?
# 2. 그냥 MongoDB에 싹 다 넣기
# gridfs를 쓸만큼 데이터의 크기가 클까?
import base64
from datetime import datetime
from pymongo import MongoClient
import json
import os
import gridfs

# MongoDB 연결
client = MongoClient('localhost', 27017)
db = client['robotData']
collection = db['Data']
fs = gridfs.GridFS(db)

json_file = 'json파일의 경로'

image_path = 'image파일의 경로'

with open(json_file, 'r') as file:
    data = json.load(file)

for item in data:
    image_path = os.path.join(image_path, item['imageData'])
    with open(image_path, 'rb') as f:
        image_id = fs.put(f, filename=item['imageData'])
    item['imageData'] = image_id

collection.insert_many(data)

print("save data")

