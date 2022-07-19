-- CreateTable
CREATE TABLE "CreditCard" (
    "id" TEXT NOT NULL,
    "number" TEXT NOT NULL,
    "expiry" TEXT NOT NULL,
    "user_id" TEXT NOT NULL,
    "document" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "createdAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "CreditCard_pkey" PRIMARY KEY ("id")
);
