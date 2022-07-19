import { IsInt, IsString } from 'class-validator';

export class ProductDto {
  @IsString()
  readonly id: string;

  @IsString()
  readonly name: string;

  @IsInt()
  readonly weight: number;

  @IsInt()
  readonly price: number;
}
