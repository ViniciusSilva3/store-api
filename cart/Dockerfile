FROM golang:alpine

ENV APP_HOME /app

WORKDIR ${APP_HOME}

COPY go.mod ./
COPY go.sum ./

RUN go mod download

COPY *.go ./

RUN go build -o /cart

CMD [ "/cart" ]
