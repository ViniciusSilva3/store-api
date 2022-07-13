build: 
	docker network create store-api-network || true
	docker-compose build

dev:
	@make build
	docker-compose up payment-service

ci:
	docker-compose run --rm ci

database: 
	docker-compose run payment-service npx prisma migrate dev --name init
