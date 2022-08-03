class CreateOrders < ActiveRecord::Migration[7.0]
  def change
    create_table :orders do |t|
      t.integer :user_id
      t.text :cart, array: true, default: []
      t.integer :payment_id
      t.decimal :payment_amount, precision: 8, scale: 2
      t.decimal :shipping_value, precision: 8, scale: 2

      t.timestamps
    end
  end
end
