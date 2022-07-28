network:
	docker network create store-api-network | exit 0

build: 
	@make network
	docker-compose build ${BUILD_TARGETS}

dev:
	@make network
	docker-compose up catalog-service

database:
# adicionar o comando de buildar a partir de uma iamgem 
#	docker exec -i catalog-catalog-db-1 psql -U postgres -d catalog < db.sql