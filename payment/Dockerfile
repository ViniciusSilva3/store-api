FROM node:latest

ENV PORT=3000
ENV APP_HOME=/app

EXPOSE $PORT
WORKDIR $APP_HOME

COPY . ${APP_HOME}

RUN yarn

CMD ["yarn", "start:dev"]
