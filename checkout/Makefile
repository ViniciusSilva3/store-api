network:
	docker network create store-api-network || true

build:
	@make network
	docker-compose build

dev:
	@make network
	@make database
	docker-compose up checkout-service

ci:
	docker-compose run --rm checkout-ci

database:
	docker-compose run checkout-service bundle exec rails db:drop db:create db:seed
