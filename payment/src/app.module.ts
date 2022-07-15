import { Module } from '@nestjs/common';
import { CreditCardController } from './creditCard/creditCard.controller';
import { CreditCardService } from './creditCard/creditCard.service';
import { PaymentService } from './payment/payment.service';
import { PrismaService } from './prisma.service';

@Module({
  imports: [],
  controllers: [CreditCardController],
  providers: [CreditCardService, PaymentService, PrismaService],
})
export class AppModule {}
