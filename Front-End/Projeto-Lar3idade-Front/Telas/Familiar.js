import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground } from 'react-native';
import base64 from 'base64-js';

export default function Familiar({ navigation, route }) {
  const { params } = route;
  const {FamiliarData, FamiliarNome} = params || {};
  const [imageData, setImageData] = useState(null);


  useEffect(() => {
    // Assuming FamiliarData.Imagem is the Buffer representing the image
    if (FamiliarData && FamiliarData.Imagem && FamiliarData.Imagem.data) {
      const base64String = base64.fromByteArray(new Uint8Array(FamiliarData.Imagem.data));
      setImageData(`data:image/png;base64,${base64String}`);
    }
  }, []);


  const handleSair = () => {
    navigation.navigate('Login');
  };

  const handleVisitas = () => {
    navigation.navigate('VisitasFamiliar');
  };

  const handlePagamentos = () => {
    navigation.navigate('PagamentosFamiliar');
  };

  const handleInfo = () => {
    navigation.navigate('UtenteFamiliar');
  };

  return (
    <View style={styles.container}>
      {imageData && <Image style={styles.image} source={{ uri: imageData }} />}
      {FamiliarNome ? (
        <Text>Bem-vindo, {FamiliarNome}!</Text>
      ) : (
        <Text>Carregando...</Text>
      )}
      <Text style={styles.ButtonText}>Teste Visitas</Text>
      <TouchableOpacity style={styles.Button} onPress={handlePagamentos}>
        <Text style={styles.ButtonText}>Plano de Pagamentos</Text>
      </TouchableOpacity>
      <TouchableOpacity style={styles.Button} onPress={handleInfo}>
        <Text style={styles.ButtonText}>Informações de Utentes </Text>
      </TouchableOpacity>
      <TouchableOpacity style={styles.Button} onPress={handleVisitas}>
        <Text style={styles.ButtonText}>Visitas</Text>
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
  Image1:{
    height:110,
    width:400,
    marginTop:-155
    
    
  },
  Image2: {
    height:205,
    width:410,
    padding:20,
    marginBottom:-250,
   
  },
  Imag:{
    padding:100,
    marginBottom:-200,

  },
  Imag1:{
    padding:195,
  },
  sairButton: {
    backgroundColor: 'white',
    padding: 2,
    width:60,
    borderRadius: 5,
    alignSelf: 'flex-end', 
    margin: 40,
  },
  sairButtonText: {
    color: 'black',
    fontWeight: 'bold',
    textAlign:'center'
  },
  image:{
    width: 100,  
    height: 100, 
    resizeMode: 'contain',
    marginBottom: 20,
    alignItems: 'center'
  }
});