#En este apartado tenemos las condiconales
#Los condionales son flujos de ejecución del código por bloques
my_conditional = True
if my_conditional:
    print("Se ejecuta este bloque if")

print("La ejecución puede continuar..")
#Dentro de las condicones se usa los boolenanos otro ejemplo tenemos
my_conditional_false = False#Los condicionales solo imprime el true no false
if my_conditional_false:
    print("Este no va a imprimir por el bool false")
print("No puede imprimir")
#Otra forma de declarar
my_conditional = 5 * 5
if my_conditional == 10:#En este caso no imprime por que 5*2=10 no 25
    print("Se puede ejecutar la condicional de la multiplicación")
print("Si se ejecuto")
if my_conditional >= 10:#En este si se imprime por que 5*2 debe ser 10 o mayor de 10
    print("Se puede ejecutar la condicional del mayor o igual")
print("Si se ejecuto")
#Tenemos la otra condición donde vemos si cumple o no es if-else
if my_conditional > 10 and my_conditional < 30:
    print("El resultado correcto es mayor a 10 y menor que 30")
elif my_conditional == 25:
    print("El resultado es igual a 25")
else:
    print("El resultado es menor o igual que 10 o mayor o igual que 25 ")
print("Esto siempre se imprime")
#Otro ejemplo mas simpificado
my_second_conditional = 10 * 10
if my_second_conditional == 150:
    print("El resultado es correcto")
elif my_second_conditional == 100:
    print("El resultado es mas que correcto") 
else:
    print("El resultado esta mal")
print("Esta es la segunda condicional")
#Tenemos un ejemplo con una cadena de texto
my_string = "Mi nombre es Stalin Vega"
if my_string:
    print("Mi cadena de texto no esta vacia...")
print("Impresión de una cadena de Texto")
#Otra manera de imprimir una cadena de texto dentro de una condicional
if my_string == "Mi segundo nombre es Bladimir":
    print("Se hace la impresión de mi segundo nombre") #Esto se la impresion segun el bool que se ponga

#Como imprimir una cadena de texto vacia en una condiconal
my_string = ""
if not my_string:
    print("Mi cadena de texto no esta vacia")
if my_string == "Mi texto mal hecho":
    print("Este bloque de texto no va a imprimir")