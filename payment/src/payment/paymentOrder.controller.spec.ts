import { Test, TestingModule } from '@nestjs/testing';
import { Prisma } from '@prisma/client';
import { PaymentOrderController } from './paymentOrder.controller';
import { PaymentService } from './payment.service';

describe('PaymentOrderController', () => {
  let paymentOrderController: PaymentOrderController;
  // let paymentService: PaymentService;

  const payments: Prisma.PaymentCreateInput[] = [
    {
      amount: 20000,
      status: 'completed',
      order_id: '123456789',
      installments: 1,
      user_id: '123456789',
    },
  ];

  beforeEach(async () => {
    const app: TestingModule = await Test.createTestingModule({
      controllers: [PaymentOrderController],
      providers: [
        {
          provide: PaymentService,
          useValue: {
            payments: jest.fn(() => payments),
          },
        },
      ],
    }).compile();

    // paymentService = app.get<PaymentService>(PaymentService);
    paymentOrderController = app.get<PaymentOrderController>(
      PaymentOrderController,
    );
  });

  it('should be defined', () => {
    expect(paymentOrderController).toBeDefined();
  });
  describe('getPaymentByOrder', () => {
    it('should get payments', async () => {
      const res = await paymentOrderController.getPaymentByOrderId(
        payments[0].order_id,
      );

      expect(res).toEqual(payments);
    });
  });
});
