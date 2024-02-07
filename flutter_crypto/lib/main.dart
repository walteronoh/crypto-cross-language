import 'package:flutter/material.dart';
import 'package:flutter_crypto/utils/util.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Crypto',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: const MyHomePage(title: 'Flutter Crypto Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  Utils utils = Utils();

  @override
  void initState() {
    // TODO: implement initState
    encryptAndDecrypt();
    super.initState();
  }

  void encryptAndDecrypt() async {
    var encrypted = await utils.encrypt();
    // {tag: 1vPV4ZxLnxiGczULsiQCeQ==, encryptedData: KGRs+XPv74JaNieFk20YjRdYDLf/WF/2km3LDUye6I7yxK2WUugqPYsmB/Y=, iv: 0PKPGtA8pJ/cBapWmbJoEA==}
    var decrypted = await utils.decrypt(
        encrypted["encryptedData"], encrypted["iv"], encrypted["tag"]);
    print(decrypted);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: const Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text(
              'Flutter Crypto:',
            ),
          ],
        ),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }
}
