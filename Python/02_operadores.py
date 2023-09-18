
#En el caso de hacer la suma por ejemplo dentro de la suma
print(3+4)
print(7+8)
print(9-6)#Resta
print(9*8)#Multiplicacion
print(9/8)#Division
print(15 % 3)#Modulo
print(10//5)#Division entera
print(5**5)#Operacion exponencial 
#Imprimir dos cadenas de texto en concatenación por ejemplo
print("Hola" + " Stalin")
#Dentro de la concatenación de los textos solo funciona dentro de "+" los demas operadores no funciona
#En el caso no se puede mezclar una cadena de texto con un entero de esta manera
#print("Stalin" + 5) #Esto no funciona en Python por que es un lenguaje tipado 
#Funciona esto
print("Stalin " + str(5))# Es donde un dato hacemos la transformacion de un string a un entero 
#En el caso de que quiera multiplicar una palabra en mi caso mi nombre que me aprezca 5 veces lo hacemos de la siguiente manera
print("Stalin: " * 10)
#En el caso de que quiera hacer una cadena de texto dentro de los operadores de la potencia
print("Stalin" * (2**5))
#En el caso de convertir de float a entero
my_float = 2.5 *5
print("Stalin  " * int(my_float))
#De igual menera tenemos los operadores comparativos es decir el mayor que y menor que 
print(6 > 9)
print(5<6)
print(9 >= 5)
print(41 <=  8)
print(6==10)
print(5!=6)
#Estos son los sognos comperativos que mas se lo utilizan dentro de python
#En este tipo de operadores se manera en el tipo de dato bool "booleano" es 
#decir que solo nos retorna un true o false
#################
#Haciendo pruebas de caja blanca es donde se puede observar el codigo 
print(55 > 5 == 5)
print("Hola python" > "Hola mundo")
print("Hola mundo" < "Hola python")
print("python" >= "mundo")
print("python" <=  "python")
print("hola"=="hola")
print("python"!="python")
#Dentro del caso de los operadores comparativos que se usan en la
#cadena de caracteres lo hace en el orden alfabetica por ejemplo
print(len("aaaa") == len("abcd"))
#Por ultimo tenemos los operadores lógicos 
#dentro de estos operadores tenemos and, or y not
print(6 > 9 and "Hola mundo" > "Hola python")
print(5 > 6 or "Hola" > "Python")
print(9 < 5 and "Hola" < "Python")
print(41 <  8 or "Hola" < "Python")
print(41 <  8 or "Hola" < "Python" and 4 == 4)
print(not(3 > 4 ))
#Dentro del leguaje de Python se escribe and y no &&