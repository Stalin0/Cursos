#Definion de los set son array pero en python no existe los array son listas directamente
my_set = set()
my_other_set = {"23", "1.72", "Stalin", "Vega", "Apunte"}
#Dentro de las declarar los set se lo hace con corchetes
print(type(my_set))
print(my_other_set)
print(type(my_other_set))
#En el caso de que no se ingresen datos Python lo puede interpretar como diccionario
#Imprimimos el numero de elementos de un set
print(len(my_other_set))
#Para poder agregar dentro de un set se usa la palabra "add"
my_other_set.add("Pepe")
print(my_other_set)#En con esto solo introducimos el elemento 
my_other_set.add("Pepe")
print(my_other_set)
#Un set no es una estructura ordenada y no admite datos repetidos
print("Bladimir" in my_other_set)#Es la forma de buscar un dato en el sets
print("Galo" in my_other_set)
print("Stalin" in my_other_set)
#Para hacer la eliminación de los elementos de un set hacemos
my_other_set.remove("Stalin")
print(my_other_set)
#Se puede eliminar todo lo podemos hacer con "Clear"
my_other_set.clear()#Vacia todos los elementos 
print(len(my_other_set))
#La funcion "Del" elimina todo el objeto del sets
del my_other_set
#print(my_other_set)#NameError: name 'my_other_set' is not defined
my_set = {"Alan", 22, 1.80}
my_list = list(my_set)#Conversion a una lista
print(my_list)
#Busqueda por posicion de un elemento en una lista
print(my_list[0])
#Creacion de otro set
my_other_set = {"Dll", "basquet", "Ecuaboli", "bmx"}
#Para unir un set usamos la palabra reservada que es sets
my_new_set = my_set.union(my_other_set)
print(my_new_set.union(my_new_set).union(my_set).union({"Futbol", "VideoJuegos"}))
#Otra funcion que podemos utilizar es la diferencia 
print(my_new_set.difference(my_set))