import unittest 
import copy
import database as db 
import helpers
import config
import csv



class TestDatabase(unittest.TestCase):

    def setUp(self) -> None:
        db.Clientes.lista = [
            db.Cliente("15J", "Marta", "Perez"),
            db.Cliente("48H", "Manolo", "Lopez"),
            db.Cliente("28Z", "Pepe", "Romano")
        ]
        

    def test_buscar_cliente(self):
        cliente_existente = db.Clientes.buscar("15J")
        cliente_inexistente = db.Clientes.buscar("99X")
        self.assertIsNotNone(cliente_existente)
        self.assertIsNone(cliente_inexistente) 


    def test_modificar_cliente(self): 
        cliente_a_modificar = copy.copy(db.Clientes.buscar("15J"))
        cliente_modificado = db.Clientes.modificar("15J", "Mariana", "Garcia")
        self.assertEqual(cliente_a_modificar.nombre, "Marta")
        self.assertEqual(cliente_modificado.nombre, "Mariana")


    def test_borrar_cliente(self): 
        cliente_borrado = db.Clientes.borrar("48H")
        cliente_rebuscado = db.Clientes.buscar("48H")
        self.assertEqual(cliente_borrado.dni, "48H")
        self.assertIsNone(cliente_rebuscado)
    


    def test_crear_cliente(self):
        nuevo_cliente = db.Clientes.crear("39X", "Hector", "Costa")
        self.assertEqual(len(db.Clientes.lista), 4)
        self.assertEqual(nuevo_cliente.dni, "39X")
        self.assertEqual(nuevo_cliente.nombre, "Hector")
        self.assertEqual(nuevo_cliente.apellido, "Costa")


    def test_dni_valido(self):
        self.assertTrue(helpers.dni_valido("00A", db.Clientes.lista))
        self.assertFalse(helpers.dni_valido("2323235", db.Clientes.lista))
        self.assertFalse(helpers.dni_valido("F35", db.Clientes.lista))
        self.assertFalse(helpers.dni_valido("48H", db.Clientes.lista))

    def test_escritura_csv(self): 
        db.Clientes.borrar("48H")
        db.Clientes.borrar("15J")
        db.Clientes.modificar("28Z", "Mariana", "Garcia")

        dni, nombre, apellido = None, None, None

        with open(config.DATABASE_PATH, newline="\n") as fichero: 
            reader = csv.reader(fichero, delimiter=";")
            dni, nombre, apellido = next(reader)

        self.assertEqual(dni, "28Z")
        self.assertEqual(nombre, "Mariana")
        self.assertEqual(apellido, "Garcia")    









