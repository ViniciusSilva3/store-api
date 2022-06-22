FROM golang:alpine

ENV APP_HOME /app
ENV PORT 8000

WORKDIR ${APP_HOME}

COPY go.mod ./
COPY go.sum ./

RUN go mod download

COPY *.go ./

EXPOSE ${PORT}

CMD [ "go", "run", "main.go" ]
