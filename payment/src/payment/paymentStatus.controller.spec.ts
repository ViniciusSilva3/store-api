import { Test, TestingModule } from '@nestjs/testing';
import { Prisma } from '@prisma/client';
import { PaymentStatusController } from './paymentStatus.controller';
import { PaymentService } from './payment.service';

describe('PaymentStatusController', () => {
  let paymentStatusController: PaymentStatusController;
  // let paymentService: PaymentService;

  const payment: Prisma.PaymentCreateInput = {
    amount: 20000,
    status: 'completed',
    order_id: '123456789',
    installments: 1,
    user_id: '123456789',
  };

  const paymentUpdate = {
    status: 'completed',
  };

  beforeEach(async () => {
    const app: TestingModule = await Test.createTestingModule({
      controllers: [PaymentStatusController],
      providers: [
        {
          provide: PaymentService,
          useValue: {
            updatePayment: jest.fn(() => payment),
            payment: jest.fn(() => payment),
          },
        },
      ],
    }).compile();

    // paymentService = app.get<PaymentService>(PaymentService);
    paymentStatusController = app.get<PaymentStatusController>(
      PaymentStatusController,
    );
  });

  it('should be defined', () => {
    expect(paymentStatusController).toBeDefined();
  });

  describe('updatePaymentStatus', () => {
    it('should update payment status', async () => {
      const res = await paymentStatusController.updatePaymentStatus(
        payment.id,
        paymentUpdate,
      );

      expect(res).toEqual(payment);
    });
  });
});
