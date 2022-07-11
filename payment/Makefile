build: 
	docker network create store-api-network || true
	docker-compose build

dev:
	@make build
	docker-compose up payment-service

ci:
	docker-compose run --rm ci