#En este apartado vamos hacer el tema de las tuplas
#Definición de una tupla es un conjunto de valores
my_tuple = tuple()#Como se define 
my_other_tuple = ("Bladimir", "Apunte")#Otra manera de definir
my_tuple = (23, 1.72, "Stalin", "Vega", "Sangolqui")
print(my_tuple)
print(type(my_tuple))
#Imprimir segun la posicion que se encuentre los valores
print(my_tuple[1])
print(my_tuple[-1])
#Para la impresion de las tuplas segun la posicion es necesario revisar los datos que se tenga
#Como contar los elementos dentro de una tupla
print(my_tuple.count("Stalin"))
#Podemos ver el posicionamiento de los valores de las tuplas con "index"
print(my_tuple.index("Vega"))
#agregar datos a una tupla no se puede por que es estatico no se pueden agregar
#my_tuple[1] = "Bladimir"#--Esto es un error
#print(my_tuple)#Por lo tanto no hay impresion
print(my_tuple + my_other_tuple)
#Otra manera de llamar
my_new_tuple = my_tuple + my_other_tuple
print(my_new_tuple)
print(my_new_tuple[1:5])
#Conclusion los valores son estaticos por eso se usa tuplas caso contrario se usa listas
#Tambien podemos hacer que la tupla se me haga un lista por ejemplo
my_tuple = list(my_tuple)
print(my_tuple)
print(type(my_tuple))
#Aqui si podemos modificar la tupla al momento de pasar a una lista
my_tuple[4] = "Azul"
my_tuple.insert(1, "Rojo")
print(tuple(my_tuple))
#Dentro de las tuplas no se pueden borrar 
#del my_tuple#No podemos hacer esto dentro de las tuplas
#print(my_tuple) #Este es un error por que nname 'my_tuple' is not defined