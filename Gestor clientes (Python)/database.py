#Controlar los datos, y dar una interfaz
import csv
import config

class Cliente: #Son los clientes individuales
    def __init__(self, dni, nombre, apellido) -> None:
        self.dni= dni
        self.nombre= nombre
        self.apellido= apellido 


    def __str__(self) -> str:
        return f"({self.dni}) {self.nombre} {self.apellido}"
    

class Clientes: 

    lista = [] #Clientes solo tiene una lista, para cualquier instancia de Clientes

    with open(config.DATABASE_PATH, newline="\n") as fichero: 
        reader = csv.reader(fichero, delimiter=";") 
        for dni, nombre, apellido in reader: 
            cliente = Cliente(dni, nombre, apellido) 
            lista.append(cliente)






    #static method

    def buscar(dni):
        for cliente in Clientes.lista: 
            if cliente.dni == dni:
                return cliente


    #static method
    def crear(dni, nombre, apellido): 
        cliente = Cliente(dni, nombre, apellido)
        Clientes.lista.append(cliente)
        Clientes.guardar()
        return cliente
    
    #static method
    def modificar(dni, nombre, apellido):
        for indice, cliente in enumerate(Clientes.lista): 
            if cliente.dni == dni:
                Clientes.lista[indice].nombre = nombre 
                Clientes.lista[indice].apellido = apellido
                Clientes.guardar()
                return Clientes.lista[indice]


    #static method
    def borrar(dni): 
        for indice, cliente in enumerate(Clientes.lista):
            if cliente.dni == dni:
                
                cliente_borrado = Clientes.lista.pop(indice) 
                Clientes.guardar()
                return cliente_borrado
            

    @staticmethod 
    def guardar():
        with open(config.DATABASE_PATH, "w", newline="\n") as fichero:
            writer = csv.writer(fichero, delimiter=";")
            for cliente in Clientes.lista: 
                writer.writerow((cliente.dni, cliente.nombre, cliente.apellido))



