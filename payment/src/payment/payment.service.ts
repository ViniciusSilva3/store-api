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

  async updatePayment(params: {
    where: Prisma.PaymentWhereUniqueInput;
    data: Prisma.PaymentUpdateInput;
  }): Promise<Payment> {
    const { where, data } = params;
    return this.prisma.payment.update({
      data,
      where,
    });
  }

  async deletePayment(where: Prisma.PaymentWhereUniqueInput): Promise<Payment> {
    return this.prisma.payment.delete({
      where,
    });
  }

  async payments(
    where?: Prisma.PaymentWhereInput,
    orderBy?: Prisma.PaymentOrderByWithRelationInput,
    skip?: number,
    take?: number,
  ): Promise<Payment[]> {
    return this.prisma.payment.findMany({
      where,
      orderBy,
      skip,
      take,
    });
  }

  async payment(
    where: Prisma.PaymentWhereUniqueInput,
  ): Promise<Payment | null> {
    return this.prisma.payment.findUnique({
      where,
      include: {
        CreditCard: true,
      },
    });
  }
}
