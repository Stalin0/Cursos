void main() {
  print(greetEveryone());
  print('Suma: ${addTwoNumbers(10, 20)}');
  print('Suma opcional: ${addTwoNumbersOptional(10)}');
  print(greetPerson(name: 'Stalin', message: 'Hi,'));
}

String greetEveryone() => 'Hello everyone!';

// int addTwoNumbers(int a, int b) {
//   return a + b;
// }
int addTwoNumbers(int a, int b) => a + b;

int addTwoNumbersOptional(int a, [int b = 0]) {
  //   b = b ?? 10;
  return a + b;
}

String greetPerson({required String name, String message = 'Hola, '}) {
  return '$message Fernando';
}
