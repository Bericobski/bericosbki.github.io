#Contendra funciones auxiliares de uso general/comunes. Utilizadas en varios scripts

import os
import platform 
import re #Expresiones regulares

def limpiar_pantalla(): 
    os.system("cls") if platform.system() == "Windows" else os.system("clear")  #Para ejecutar la opcion correcta de limpiar pantalla, sea windows o linux/mac



def leer_texto(longitud_min=0, longitud_max=100, mensaje=None): 
    print(mensaje) if mensaje else None #Si mensaje existe, se imprime, sino, no hace nada
    while True: 
        texto = input("> ")
        if len(texto) >= longitud_min and len(texto) <= longitud_max:
            return texto
        

def dni_valido(dni, lista):
    if not re.match("[0-9]{2}[A-Z]$", dni): #Expresion regular
        print("DNI incorrecto, debe cumplir el formato.")
        return False
    for cliente in lista:
        if cliente.dni == dni:
            print("DNI ultilizado en otro cliente")
            return False
    return True





