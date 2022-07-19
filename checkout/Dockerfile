FROM ruby:3.0.0

ENV PORT=3001
ENV APP_HOME=/app

EXPOSE $PORT
WORKDIR $APP_HOME

COPY . ${APP_HOME}

RUN gem install bundler && \
    bundle install

CMD ["rails", "server", "-p", "3001", "-b", "0.0.0.0"]
