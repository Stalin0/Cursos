#En este apartado vamos hacer los diccionarios
#Creación de un diccionario en si utiliza un hagmaps
my_dict = dict()
my_other_dict = {}
print(type(my_dict))
print(type(my_other_dict))

#Creación de un diccionario
my_other_dict = {"Nombre": "Stalin", "Apellido": "Vega", "Edad":23, 1:"Python"}
print(my_other_dict)
#Creacion de un diccionario en formato de json
my_dict = {
    "Nombre": "Stalin",
    "Apellido": "Vega",
    "Edad": 23,
    "Deportes": {"Futbol", "Basquet", "Ecuaboli"},
    1:1.72#Este funciona como ID
}
print(my_dict)
#Con la función de "len" hacemos el conteo de los datos
print(len(my_other_dict))
print(len(my_dict))
print(my_dict["Deportes"])
print(my_dict[1])
#Agregación de los datos
my_dict["Via"] = "Echeandia"
print(my_dict)
#Tambien podemos hacer la eliminación de los datos
del my_dict["Via"]
print(my_dict)
#Comprobación de un dato existente dentro de un diccionario
#La buqueda de un elemento de python se buscar por la variable crear y no su valor
print("Stalin" in my_dict)
print("Stalin" in my_other_dict)
#La forma correcta de buscar dentro de un diccionario
print("Apellido" in my_dict)
#Con esto podemos ir a diferentes valores
print(my_dict.keys())#Con esto retornamos todas las claves
print(my_dict.values())#El contenido que esta dentro de las claves
#print(my_dict.fromkeys())#TypeError: fromkeys expected at least 1 argument, got 0
print(my_dict.items())#Esto imprime toda la cadena de un dicicionario
#Dentro de la función de "fromkeys" debemos pasar las claves a buscar es decir
print(my_other_dict.fromkeys("Nombre", 1))
#Otra manera de llamar la función
my_new_dict = my_other_dict.fromkeys(("Nombre",1,"Piso"))
print(my_new_dict)
#Tambien podemos meter una lista dentro de un diccionario
my_list = ["Nombre",1,"Piso"]
my_new_dict = dict.fromkeys(my_list)
print(my_new_dict)
my_new_dict = dict.fromkeys(my_dict)
print(my_new_dict)
#Agregacion de valores a la cadena del diccionario
my_new_dict = dict.fromkeys(my_dict,("Patricio", "Adrian"))
print(my_new_dict)
#Dentro de cada elemento se ingresa al dicicionario completo
##################
#Tambien podemos transformar el diccionario en una lista, tupla y un set
print(my_new_dict.values())
#Como nos queda el tipo dentro de la función "Values"
my_values = my_new_dict.values()
print(type(my_values))
print(list(my_new_dict))
print(tuple(my_new_dict))
print(set(my_new_dict))
#Algo complejo con todas las operaciones
print(list(dict.fromkeys(list(my_new_dict.values())).keys()))