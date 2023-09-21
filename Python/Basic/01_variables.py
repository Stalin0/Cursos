#Dentro de python las variables se las puden crear de la siguiente manera 
#Variable declarada dentro de una str
my_string_variable = "Hola mundo"
print(my_string_variable)
#Variable tipo entero
my_int_variable = 5
print(my_int_variable)
#Variable tipo booleano
my_bool_variable = True
print(my_bool_variable)

#Otra manera de llamar una funcion es decir que una existente llame a una nueva ejemplo
my_int_to_str_variable = str(my_int_variable)
print(my_int_to_str_variable)
print(type(my_int_to_str_variable))

print(my_string_variable,my_int_variable,my_bool_variable)
#Otro tipo de funcion que me cuenta la cadena de caracteres
print(len(my_string_variable))
#Existe las variables que se declaran dentro de una sola linea
name, age, address = "Stalin", 23, "Guanujo"
print("Yo me llamo:", name, " Mi edad es: ",age , " Yo vivo dentro de:", address)
#Podemos manejar tambien los input que es donde va la entrada de los tipos de datos 
#por ejemplo tenemos lo siguiente
name = input('Dijite por favor su nombre:')
age = input('Dijite su años:')
address = input('Ingrese su direccion de vivienda donde se encuentra:')
print("OK", "Su nombre es:", name, "Tiene",age, "años", "y se encuentra en:", address)

#Cambiando el tipo de los datos por ejemplo
name = 23
age = "Stalin"
print(name)
print(age)
#Forzamos al tipo de dato por ejemplo
address: str = "Donde yo vivo"
address = True
address = 23
address = 1.2
print(type(address))