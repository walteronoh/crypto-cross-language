import { Body, Controller, Get, HttpStatus, Post, Req, Res } from '@nestjs/common';
import { AppService } from './app.service';
import { Request, Response } from 'express';
import { DecryptDto } from './app.dto';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) { }

  @Get()
  getHello(): string {
    return this.appService.getHello();
  }

  @Get("/name")
  getName(): string {
    return "Walter";
  }

  @Post("/encrypt")
  encrypt(@Req() request: Request, @Res() response: Response) {
    return response.json(this.appService.encrypt(request.body));
  }

  @Post("/decrypt")
  decrypt(@Body() body: DecryptDto, @Res() response: Response) {
    return response.json(this.appService.decrypt(body.iv, body.encryptedData, body.tag));
  }
}
