class CatalogService

  def api_connection()
    Faraday.new(url: ENV.fetch('CATALOG_URL')) do |conn|
      conn.request :json
      conn.response :json
      conn.adapter Faraday.default_adapter
    end
  end

  def get(product_id)
    api_connection().get do |request|
      request.url "catalog/product/#{product_id}"
    end
  end

  def get_multiple_products(product_ids)
    product_list = {}
    product_ids.each do |product_id|
      product_list[product_id] ||= self.get(product_id)
    end
    product_list
  end
end
