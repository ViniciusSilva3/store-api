import { Module } from '@nestjs/common';
import { CreditCardController } from './creditCard/creditCard.controller';
import { CreditCardService } from './creditCard/creditCard.service';
import { PrismaService } from './prisma.service';

@Module({
  imports: [],
  controllers: [CreditCardController],
  providers: [CreditCardService, PrismaService],
})
export class AppModule {}
