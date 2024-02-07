import { Injectable } from '@nestjs/common';
import { createCipheriv, createDecipheriv, randomBytes } from 'crypto';

@Injectable()
export class AppService {
  private key: string = "abcdefghijklmnopqrstuvwxyz123456"; // Should be 32 chars long

  getHello(): string {
    return 'Hello World!';
  }

  encrypt(object: Record<string, any>): Record<string, string> {
    let iv = randomBytes(16);

    const cipher = createCipheriv("aes-256-gcm", this.key, iv);

    let encrypted = cipher.update(JSON.stringify(object), "utf8", "base64");
    encrypted += cipher.final("base64");

    const tag = cipher.getAuthTag();

    return {
      iv: iv.toString("base64"),
      encryptedData: encrypted,
      tag: tag.toString("base64")
    }
  }

  decrypt(iv: string,encryptedData: string, tag: string) {
    const decipher = createDecipheriv("aes-256-gcm", this.key, Buffer.from(iv, "base64"));

    decipher.setAuthTag(Buffer.from(tag, "base64"));

    let decrypted = decipher.update(encryptedData, "base64", "utf8");

    decrypted += decipher.final("utf8");

    return decrypted;
  }
}
