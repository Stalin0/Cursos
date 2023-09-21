#En este apartado vamos con las clases en python
#Definir una clase, dentro de Python las clases usamos CamelCase

#class Person:
#    pass#Nos ayuda a no tener error y pasa por nulo
#pass
#print("Pass es un valor nulo")
class MyEmptyPerson:
    pass
print(MyEmptyPerson)
print(MyEmptyPerson())

class Person:
    def __init__(self, name, surname):
        #Empezamos el contructor
        self.name = name#El self se usa como nativo de python para los atributos
        self.surname = surname
my_person =Person("Stalin", "Vega")
print(f"{my_person.name} {my_person.surname}")
#Otra manera de hacer
class Person_1:
    def __init__(self, name, surname, alias = "Chulo"):
        self.full_name = f"{name} {surname} ({alias})"#Este atributo es publico
        self.__name = name

    def get_name(self):
        return self.__name
        
    def walk(self):
        print(f"{self.full_name} Esta caminando")
        
my_person = Person_1("Bladimir", "Apunte")
print(my_person.full_name)
print(my_person.get_name())
my_person.walk()

my_other_person = Person_1("Ricardo", "Lazo", "Gordita")
print(my_other_person.full_name)
my_other_person.walk()
my_other_person.full_name ="Hola Reina como estas..."
print(my_other_person.full_name)
        