build: 
	docker network create store-api-network || true
	docker-compose build ${BUILD_TARGETS}

dev:
	@make build
	docker-compose up -d cart

ci:
	docker-compose run --rm ci
