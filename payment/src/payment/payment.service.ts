import { Injectable } from '@nestjs/common';
import { PrismaService } from '../prisma.service';
import { Payment, Prisma } from '@prisma/client';

@Injectable()
export class PaymentService {
  constructor(private prisma: PrismaService) {}

  async createPayment(data: Prisma.PaymentCreateInput): Promise<Payment> {
    return this.prisma.payment.create({
      data,
    });
  }
}
