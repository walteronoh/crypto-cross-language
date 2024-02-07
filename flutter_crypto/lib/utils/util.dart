import 'dart:async';
import 'dart:convert';
import 'dart:math';
import 'dart:typed_data';

import 'package:cryptography/cryptography.dart';

class Utils {
  final String key = "abcdefghijklmnopqrstuvwxyz123456";

  Uint8List generateRandomIv() {
    final Random random = Random.secure();
    return Uint8List.fromList(
        List.generate(16, (index) => random.nextInt(256)));
  }

  FutureOr<Map<String, dynamic>> encrypt() async {
    final algorithm = AesGcm.with256bits();

    final secretKey = SecretKey(utf8.encode(key));

    // Generate a random 96-bit nonce.
    final nonce = generateRandomIv();

    const clearText = {"name": "Walter", "greetings": "Hello Walter"};

    final secretBox = await algorithm.encrypt(
        utf8.encode(jsonEncode(clearText)),
        secretKey: secretKey,
        nonce: nonce);

    return {
      "tag": base64Encode(secretBox.mac.bytes),
      "encryptedData": base64Encode(secretBox.cipherText),
      "iv": base64Encode(nonce)
    };
  }

  FutureOr<Map<String, dynamic>> decrypt(
      String encryptedData, String iv, String tag) async {
    final algorithm = AesGcm.with256bits();

    final secretKey = SecretKey(utf8.encode(key));

    final secretBox = SecretBox(base64Decode(encryptedData),
        nonce: base64Decode(iv), mac: Mac(base64Decode(tag)));

    final decrypted = await algorithm.decrypt(secretBox, secretKey: secretKey);

    final decoded = utf8.decode(decrypted);

    return json.decode(decoded);
  }
}
