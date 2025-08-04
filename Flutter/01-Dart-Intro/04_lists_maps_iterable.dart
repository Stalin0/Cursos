void main() {
  final numbers = [1, 2, 3, 4, 5, 5, 5, 5, 1, 1, 6, 7, 8, 9, 9, 10];
  print('List Original: $numbers');
  print('Length: ${numbers.length}');
  print('Index 0: ${numbers[0]}');
  print('Last Number: ${numbers.last}');
  print('Reversed Number: ${numbers.reversed}');
  final reversedNumber = numbers.reversed;
  print('Iterable: ${reversedNumber}');
  print('List: ${reversedNumber.toList()}');
  print('Set: ${reversedNumber.toSet()}');

  final numbersGreaterThan5 = numbers.where((int num) {
    return num > 5; //true
  });
  print('>5: $numbersGreaterThan5');
  print('>5: ${numbersGreaterThan5.toSet()}');
}
