class PaymentService

  def initialize(user_id: nil, credit_card_id: nil)
    @user_id = user_id
    @credit_card_id = credit_card_id
  end

  def api_connection()
    Faraday.new(url: ENV.fetch('PAYMENT_URL')) do |conn|
      conn.request :json
      conn.response :json
      conn.adapter Faraday.default_adapter
    end
  end

  def get
    api_connection().get do |request|
      request.url "creditCard/#{@credit_card_id}"
    end
  end

  def get_user_credit_cards
    api_connection().get do |request|
      request.url "/creditCard/user/#{@user_id}"
    end
  end

  def create(number, expiry, name, document)
    api_connection().post do |request|
      request.url "creditCard"
      request.body = {
        user_id: @user,
        number: number,
        expiry: expiry,
        name: name,
        document: document
      }
    end
  end

  def update(name)
    api_connection().put do |request|
      request.url "creditCard/#{@credit_card_id}"
      request.body = {
        name: name
      }
    end
  end

  def delete
    api_connection().delete do |request|
      request.url "creditCard/#{@credit_card_id}"
    end
  end
end
