network:
	docker network create store-api-network || true

build: 
	@make network
	docker-compose build

dev:
	@make network
	@make database
	docker-compose up payment-service

ci:
	docker-compose run --rm ci

database: 
	docker-compose run payment-service npx prisma migrate dev --name init
