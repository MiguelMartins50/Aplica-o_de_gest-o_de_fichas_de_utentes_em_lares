import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground } from 'react-native';
import base64 from 'base64-js';
export let uten = {};
export let iduten = 0;

export default function Utente({ navigation, route }) {
  const { params } = route;
const { UtenteData, UtenteNome } = params || {};
const [imageData, setImageData] = useState(null);
const [utenteID, setUtenteID] = useState(null);
console.log("1\n"+params);
useEffect(() => {
  if (UtenteData && UtenteData.Imagem && UtenteData.Imagem.data) {
    const base64String = base64.fromByteArray(new Uint8Array(UtenteData.Imagem.data));
    setImageData(`data:image/png;base64,${base64String}`);
  }
  uten = UtenteData || {};
  iduten = UtenteData?.idUtente;
  console.log("id:" + iduten);
}, [navigation, UtenteData]);

console.log("export:" + uten);
console.log(uten.idUtente);
  const navigateToPrescricaoUtente = () => {
    navigation.navigate('Prescrições Médicas',{ utenteData: UtenteData});
  };
  const navigateToVisitas = () => {
    navigation.navigate('Visitas',{ utenteData: UtenteData});
  };
  const navigateToConsultas = () => {
    navigation.navigate('Consultas',{ utenteData: UtenteData});
  };
  const navigateToAtividades = () => {
    navigation.navigate('Atividades',{ utenteData: UtenteData});
  };
  const navigateToPlanoPagamento = () => {
    navigation.navigate('Plano de pagamento',{ utenteData: UtenteData});
  };
  const navigateToPerfilUtente = () => {
    navigation.navigate('PerfilUtente',{ utenteData: UtenteData});
  };

  return (<View style={styles.container}>
        <View style={styles.Imag1}>
        </View>
        {imageData && <Image style={styles.image} source={{ uri: imageData }} />}
        {UtenteNome ? (
        <Text style={styles.BemVindo}>Bem-vindo, {UtenteNome}!</Text>
      ) : (
        <Text>Carregando...</Text>
      )}
      <TouchableOpacity
      style={styles.Button}
      onPress={() =>navigation.navigate('PerfilUtente', { utenteData: UtenteData, utenteNome: UtenteNome })}>
      <Text style={styles.ButtonText}>Perfil Utente</Text>
    </TouchableOpacity>
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
  image:{
    width: 100,  
    height: 100, 
    resizeMode: 'contain',
    marginBottom: 20,
    alignItems: 'center'
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
  BemVindo:{
    fontSize:17,
    marginBottom:20
  }
});