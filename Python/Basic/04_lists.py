#Las listas son las mas utilizadas dentro de la IA
#Dentro de la creación dde las listas podemos crear de la siguiente manera
#Python es un lenguaje orientado a Objetos 
my_list = list()
my_other_list = []
print(len(my_list))
my_list = [23, 43, 62, 30, 45, 10, 9]
print(my_list)
print(len(my_list))
my_other_list = [23, 1.72, "Stalin", "Vega"]
print(my_other_list)
print(type(my_list))
print(type(my_other_list))
#Un array no es lo mismo que una lista 
#Las listas es mas completa y la array es inamobible 
#En conclusion una lista es un superconjunto de arreglos 
print(my_other_list[0]) #Al poner el cero "0" me esta tomando el pimer dato de la lista
print(my_other_list[-1])#Ultimo elemento de la lista
print(my_other_list[1])
print(my_other_list[-4])#En el caso de -4 el primer dato se toma como 1
#En el caso de que se pase de numero de datos de la lista da error por ejemplo
#print(my_other_list[5])#Este es un error por que no existe en el indice esa posición
print(my_other_list.count("Vega"))#El count me cuanta los datos que se menciona en una cadena de caracteres

age, height, name, surname = my_other_list
print(name)
name, height, age, surname = my_other_list[2], my_other_list[1],my_other_list[0], my_other_list[3]
print(height)
#Tambien vale utilizar algunos operadores logicos dentro de las listas
print(my_list + my_other_list)
my_other_list.append("Apunte")#Agregar se inserta por defecto al ultimo
print(my_other_list)
my_other_list.insert(1, "azul")#Se inserta segun la posicion que se le indique
print(my_other_list)
my_other_list[1] = "Rojo"
print(my_other_list)
#En el caso de usa el signo "-" de la resta nos da un error
#print(my_list - my_other_list)
my_list.remove(30)#Elimina segun el dato indicado
print(my_list)
my_list.pop()#Elimina el ultimo elmento por defecto
print(my_list)
my_list
#Otra manera de poder escribir
print(my_list.pop())
print(my_list)
#Eliminar segun el indice
print(my_list.pop(2))
print(my_list)
#En el caso de guardar creamos una variable
my_pop_element = my_list.pop(2)
print(my_pop_element)
print(my_list)
#En mi caso como dentro de la impresion solo tengo dentro del indice las dos posiciones que es: [23, 43]
my_list
#Lista de Tipos dinamicos 
#En este caso no se puede definir como una lista es una string el ejemplo sigueinte
my_list = "Hola Mundo"
print(my_list)
print(type(my_list))
#Dentro de python no se puede crear constantes 
my_list = ["Hola Mundo"]
print(my_list)
print(type(my_list))
my_list_1 =[1, 5202, 855, 96, 65, 85, 9, 5, 47]
#Otra manera de llamamiento segun la posicion de los elementos
del my_list_1[0]#Funcion "del" que me elimina por posciones 
#Dentro de esto hemos utilizado la función "del" que hace la eliminacion por indice
print(my_list_1)
#Podemos crear una nueva variable que es la siguiente
my_new_list = my_list_1.copy()
my_list_1.clear()
print(my_list_1)
my_new_list.reverse()
print(my_new_list)
#Con la funcion del "short" lo que hacemos es ordenar la lista que tengamos
#Por Ejemplo
my_new_list.sort()
print(my_new_list)
#Tambien podemos hacer sublistas de la sigueinte manera
#Tomando la posición de la lista
print(my_new_list[1:3])


