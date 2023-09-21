#En este apartado vamos hacer las funciones
#La palabra reservada es def dentro de python
def my_function():
    print("Mi primera función dentro de Python")
my_function()#Con esto hacemos el llamamiento de la función
my_function()
#Función con la suma de dos valores
#Dentro de python usamos la progrmación dinamica y no es estatica acepta desde listas hasta tuplas
def sum_two_values(first_number:int, second_number:int):
    print(first_number + second_number)
sum_two_values(10,17)
sum_two_values(25051, 52055)
#Podemos unir dos cadenas de Texto
sum_two_values("10", "15")
sum_two_values(1.22, 5.2)

#Una función que pueda recibir parametros igual lo pueda retornar
def sum_two_values_with_return(first_value, second_value):
    my_sum = first_value + second_value
    return my_sum
my_result = sum_two_values_with_return(10,17)
print(my_result)
#Otra Función
def print_name(name, surname):
    print(f"{name} {surname}")
print_name(surname= "Vega", name="Stalin") #Podemos ordenas mediante las variables

#Tambien hay los parametros por defecto
def print_name_with_default(name, surname, age=23):
    print(f"{name} {surname}, {age}")
print_name_with_default("Stalin", "Vega")

#Ultimo ejemplo
def print_texts(*text):#Con el * podemos poner las cadenas de texto necesarias
    print(text)
print_texts("Mi primer texto en python", "Stalin", "Vega")
print_texts("Stalin")
#Podemos imprimir dentro de un ciclo
def print_texts(*texts):#Con el * podemos poner las cadenas de texto necesarias
    print(type(texts))
    for text in texts:
        print(text.upper())#Con esto podemos poner todo en mayuscula

print_texts("Mi primer texto en python", "Stalin", "Vega")
print_texts("Stalin")   
    
    
    

    