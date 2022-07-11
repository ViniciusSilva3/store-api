build: 
	docker network create store-api-network || true
	docker-compose build

dev:
	@make build
	docker-compose up -d payment-service

ci:
	docker-compose run --rm ci