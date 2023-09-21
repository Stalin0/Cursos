#En este fichero vamos hacer los modules
import my_module
my_module.sumValue(2, 5, 5)
#Importar solo una funcion concreta
my_module.printValue("Mi primer import en Python")

#Otra manera de importar
from my_module import sumValue, printValue
sumValue(52, 10, 1 )
my_module.printValue("Mi seguna importación")

import math

math.pi
print(math.pi)
print(math.sqrt(64))
print(math.asinh(515111))
print(math.pow(2,5))
#Algo mas exacto
from math import pi
print(pi)
#Otra manera
from math import pi as PI_VALUE
print(PI_VALUE)
def sum_two_values(first_value, second_value):
    print(first_value + second_value)
sum_two_values(5, 5)
    