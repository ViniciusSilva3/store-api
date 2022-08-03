class ShippingService

  def initialize(product_list)
    @product_list = product_list
  end

  def api_connection()
    Faraday.new(url: ENV.fetch('SHIPPING_URL')) do |conn|
      conn.request :json
      conn.response :json
      conn.adapter Faraday.default_adapter
    end
  end

  def create
    response = api_connection().post do |request|
      request.url "shipping/quote"
      request.body = @product_list
    end
    return JSON.parse(response.body) unless response.status != 200

    raise StandardError.new "Error when trying to create shipping"
  end
end
