#Dentro de este apartado vamos a ver los bucles o ciclos
#Los bucles nos sirve para pasar por el mismo codigo varias veces
#1. while
#En conclusión hacemos un bucle empezando en 0 y termina en l rectricción
#Que le hemos dado es decir 17
my_condition = 0
while my_condition < 10:
    print(my_condition)
    my_condition += 2#Opera de 2 en 2 hasta que se cumpla el bucle
else: #Esta condicion es opcional dentro de este bucle
    print("La condición es menos o igual a 10")
print("La ejecución continua")
#Otro ejemplo tenemos
while my_condition < 20:#Este bucle while es infinito si no le damos una restrición
    my_condition += 2
    if my_condition == 16:
        print("Se detiene la ejecución")
        break#Con esta palabra se hace de romper el bucle
    
    print(my_condition)
print("La ejecución continua..")
#Para el bucle o ciclo for
#Como ejemplo vamos a poner una lista
my_list = [20, 52, 18, 10, 56, 91]
for element in my_list:#Un for hace iteraciones imprime las llaves y no los valores dentro de los diccionarios
    print(element)
#Ejemplo con una tupla
my_tuple = (23, 1.72, "Stalin", "Bladimir", "Vega")
for element in my_tuple:
    print(element)
my_set = {"Stalin", "Bladimir", "Vega", 23}
for element in my_set:
    print(element)
my_dict = {"Nombre": "Stalin", "Segundo Nombre": "Bladimir", "Edad": 23, 1: "Python"}
#Podemos convertir un diccionario en una lista dentro de los diciconarios para imprimes los valores
for element in my_dict:
    print(element)
    if element == "Edad":
        print("Se rompe el bucle")
        continue
    else:
        print("Continua con la ejecución")
else:
    print("Este bucle for ha finalizado")

for element in my_dict:
    print(element)
    if element == "Edad":
        print("Se rompe el bucle")
        break
else:
    print("Este bucle for ha finalizado")




