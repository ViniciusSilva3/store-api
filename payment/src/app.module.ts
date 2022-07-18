import { Module } from '@nestjs/common';

// Controllers
import { CreditCardController } from './creditCard/creditCard.controller';
import { PaymentController } from './payment/payment.controller';
import { PaymentStatusController } from './payment/paymentStatus.controller';
import { PaymentOrderController } from './payment/paymentOrder.controller';

// Models
import { CreditCardService } from './creditCard/creditCard.service';
import { PaymentService } from './payment/payment.service';
import { PrismaService } from './prisma.service';

@Module({
  imports: [],
  controllers: [
    CreditCardController,
    PaymentController,
    PaymentStatusController,
    PaymentOrderController,
  ],
  providers: [CreditCardService, PaymentService, PrismaService],
})
export class AppModule {}
