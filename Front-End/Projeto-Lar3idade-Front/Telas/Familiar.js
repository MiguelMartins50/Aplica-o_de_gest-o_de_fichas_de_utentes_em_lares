import React, { useState, useEffect } from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground } from 'react-native';
import base64 from 'base64-js';
export let fam = {}; 
export let idfam = 0;



export default function Familiar({ navigation, route }) {
  const { params } = route;
  const {FamiliarData, FamiliarNome} = params || {};
  const [imageData, setImageData] = useState(null);
  const [FamiliarID, setFamiliarID]= useState(null);
  useEffect(() => {
    if (FamiliarData && FamiliarData.Imagem && FamiliarData.Imagem.data) {
      const base64String = base64.fromByteArray(new Uint8Array(FamiliarData.Imagem.data));
      setImageData(`data:image/png;base64,${base64String}`);
    }
    fam = FamiliarData || {};
    idfam = FamiliarData.idFamiliar;
    console.log("id:"+idfam);
  }, [navigation, FamiliarData]);
  console.log("export:"+fam);
  console.log(fam.idFamiliar);

  const handleSair = () => {
    navigation.navigate('Login');
  };
  const handlePerfil = () => {

    navigation.navigate('PerfilFamiliar',FamiliarData);
  };
  const handleVisitas = () => {
    setFamiliarID(FamiliarData.idFamiliar);  

    navigation.navigate('VisitasFamiliar',{FamiliarData,FamiliarID});
  };

  const handlePagamentos = () => {
    setFamiliarID(FamiliarData.idFamiliar);  

    navigation.navigate('PagamentosFamiliar',{FamiliarData,FamiliarID});
  };

  const handleInfo = async () => {
    setFamiliarID(FamiliarData.idFamiliar);  
    navigation.navigate('UtenteFamiliar', {FamiliarData, FamiliarID});
  };

  return (
    <View style={styles.container}>
      {imageData && <Image style={styles.image} source={{ uri: imageData }} />}
      {FamiliarNome ? (
        <Text style={styles.BemVindo}>Bem-vindo/a, {FamiliarNome}!</Text>
      ) : (
        <Text>Carregando...</Text>
      )}
      <TouchableOpacity
      style={styles.Button}
      onPress={handlePerfil}>
      <Text style={styles.ButtonText}>Perfil</Text>
    </TouchableOpacity>
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
    marginBottom:-270,

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
  },
  BemVindo:{
    fontSize:17,
    marginBottom:20
  }
});