import React from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground } from 'react-native';

export default function Utente({ navigation }) {
  const navigateToPrescricaoUtente = () => {
    navigation.navigate('Prescrições Médicas');
  };
  const navigateToVisitas = () => {
    navigation.navigate('Visitas');
  };
  const navigateToConsultas = () => {
    navigation.navigate('Consultas');
  };
  const navigateToAtividades = () => {
    navigation.navigate('Atividades');
  };
  const navigateToPlanoPagamento = () => {
    navigation.navigate('Plano de pagamento');
  };

  return (
      <View style={styles.container}>
        <View style={styles.Imag1}>
        </View>

        <TouchableOpacity style={styles.Button} onPress={navigateToPrescricaoUtente}>
          <Text style={styles.ButtonText}>Prescrições Médicas</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.Button}onPress={navigateToVisitas}>
          <Text style={styles.ButtonText}>Visitas</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.Button} onPress={navigateToConsultas}>
          <Text style={styles.ButtonText}>Consultas </Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.Button} onPress={navigateToAtividades} >
          <Text style={styles.ButtonText}>Atividades</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.Button} onPress={navigateToPlanoPagamento}>
          <Text style={styles.ButtonText}>Plano de pagamento</Text>
        </TouchableOpacity>
        <StatusBar style="auto" />

        <View style={styles.Imag}>
          <Image source={require('../Image/Image2.png')} style={styles.Image2} />
        </View>
      </View>
   
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
  
  },
  Button: {
    backgroundColor: '#3498db',
    paddingVertical: 10,
    paddingHorizontal: 20, 
    borderRadius: 8,
    height: 40,
    width: 300,  
    marginBottom: 20,
    justifyContent: 'center',  
    alignItems: 'center',    
  },
  ButtonText: {
    color: '#fff',
    fontSize: 17,

  },
  Image2: {
    height:205,
    width:410,
    padding:20
   
  },
  Imag:{
    padding:50
  },
  Imag1:{
    padding:180,
    
  },
  sairButton: {
    backgroundColor: '#3498db',
    padding: 2,
    width:60,
    borderRadius: 5,
    marginLeft:310,
    top:-50,
    margin: 25,
  },
  sairButtonText: {
    color: 'black',
    fontWeight: 'bold',
    textAlign:'center'
  },
});