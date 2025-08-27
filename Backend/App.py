import datetime
import json
import MySQLdb as mysql
import pathlib
import re
import urllib.parse as urlparse
import wsgiref.types as wsgitypes

HOST = "BGJ20252.mysql.eu.pythonanywhere-services.com"
USER = "BGJ20252"
PASSWORD = "MYSQLBGJ 2025.2"
DATABASE = "BGJ20252$Leaderboard"

FIELD_TYPE = mysql.constants.FIELD_TYPE

CONVERTERS = {
    FIELD_TYPE.LONG: int,
    FIELD_TYPE.DATETIME: str,
}


class App:
    def __init__(self):
        self.connection = mysql.connect(host=HOST, user=USER, password=PASSWORD, database=DATABASE, conv=CONVERTERS, use_unicode=True, charset="utf8")
        self.cursor: mysql.cursors.Cursor = self.connection.cursor()
        self.dbExistsFlagFile = pathlib.Path("DB_IS_CONSTRUCTED")

        if not self.dbExistsFlagFile.exists():
            self.cursor.execute("DROP TABLE IF EXISTS leaderboard;")
            self.cursor.execute(
                "CREATE TABLE leaderboard ( \
                id INT NOT NULL AUTO_INCREMENT, \
                name VARCHAR(200) NOT NULL, \
                score INT NOT NULL, \
                timestamp DATETIME NOT NULL, \
                PRIMARY KEY(id) \
                );"
            )
            self.connection.commit()
            self.dbExistsFlagFile.touch()

    def getLeaderboard(self):
        self.cursor.execute("SELECT * FROM leaderboard ORDER BY score DESC, timestamp ASC LIMIT 100;")
        content = json.dumps(self.cursor.fetchall(), ensure_ascii=False)
        return {
            "status": 200,
            "respType": "application/json",
            "content": content,
        }

    def submit(self, name: str, score: int):
        self.cursor.execute("INSERT INTO leaderboard (name, score, timestamp) VALUES (%s, %s, %s);", (name, score, datetime.datetime.now()))
        self.connection.commit()

    def deleteAll(self):
        self.cursor.execute("DELETE FROM leaderboard;")
        self.connection.commit()

    def app(self, environ: wsgitypes.WSGIEnvironment, start_response: wsgitypes.StartResponse):
        path = environ.get("PATH_INFO")
        params = urlparse.parse_qs(environ.get("QUERY_STRING"))

        status = "500 INTERNAL SERVER ERROR"
        content = "No status set happened."
        responseType = "text/html"

        if path == "/getleaderboard":
            response = self.getLeaderboard()
            status = response["status"]
            content = response["content"]
            responseType = response["respType"]
        elif path == "/submit":
            name = params.get("name", [None])[0]
            score = params.get("score", [None])[0]
            if not name or not score:
                status = "400 BAD REQUEST"
                content = "Missing name or score."
                responseType = "text/html"
            elif not re.match(r"^\w+$", name) or len(name) > 200:
                status = "400 BAD REQUEST"
                content = "Invalid name."
                responseType = "text/html"
            elif not re.match(r"^\d+$", score):
                status = "400 BAD REQUEST"
                content = "Invalid score."
                responseType = "text/html"
            else:
                self.submit(name, int(score))
                status = "200 OK"
                content = ""
                responseType = "text/html"
        elif path == "/deleteall":
            self.deleteAll()
            status = "200 OK"
            content = ""
            responseType = "text/html"
        else:
            status = "404 NOT FOUND"
            content = "Page not found."
        response_headers = [("Content-Type", responseType), ("Content-Length", str(len(content)))]
        start_response(status, response_headers)
        yield content.encode("utf8")
