-- AddForeignKey
ALTER TABLE "Payment" ADD CONSTRAINT "Payment_credit_card_id_fkey" FOREIGN KEY ("credit_card_id") REFERENCES "CreditCard"("id") ON DELETE RESTRICT ON UPDATE CASCADE;
