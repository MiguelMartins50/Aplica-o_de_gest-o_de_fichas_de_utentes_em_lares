import React, { useState, useEffect } from 'react';
import { StyleSheet, ImageBackground, Text, View, ScrollView ,Image} from 'react-native';
import base64 from 'base64-js';

export default function PerfilUtente({ route, navigation }) {
  const { params } = route;
  const { FamiliarData, FamiliarNome } = params || {};
  const numeroCC = FamiliarData?.numero_cc || {};
  const dataValidade = FamiliarData?.data_validade || {};
  const telCasa = FamiliarData?.telefone_casa || {};
  const tel = FamiliarData?.telemovel || {};
  const email = FamiliarData?.email || {};
  const [imageData, setImageData] = useState(null);

  useEffect(() => {
    if (FamiliarData && FamiliarData.Imagem && FamiliarData.Imagem.data) {
      const base64String = base64.fromByteArray(new Uint8Array(FamiliarData.Imagem.data));
      setImageData(`data:image/png;base64,${base64String}`);
    }
  }, [FamiliarData]);

  return (
    <View style={styles.container}>
      <ImageBackground source={require('../Image/Image2.png')} style={[styles.Image2, styles.bottomImage]} />

      {FamiliarNome ? (
        <ScrollView style={styles.PerfilUtente1}>
          {imageData && <Image style={styles.image} source={{ uri: imageData }} />}
          <Text style={styles.TEXTO}>Nome: {FamiliarNome}</Text>
          <Text style={styles.TEXTO}>Número do CC: {numeroCC}</Text>
          <Text style={styles.TEXTO}>Data de validade: {dataValidade}</Text>
          <Text style={styles.TEXTO}>Telemóvel: {tel}</Text>
          <Text style={styles.TEXTO}>Telefone casa: {telCasa}</Text>
          <Text style={styles.TEXTO}>Email: {email}</Text>
        </ScrollView>
      ) : (
        <Text>Carregando...</Text>
      )}
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
  TEXTO: {
    marginBottom: 7,
    borderWidth: 1,
    borderColor: 'black',
    padding: 6,
    width:'auto',
    borderRadius: 8,
    textAlign: 'center',
  },
  PerfilUtente1: {
    flex: 1,
    padding: 10,
    marginBottom: 50,
    marginTop: 10, 
    
  },
  Image2: {
    height: 205,
    width: '110%', 
    padding: 10,
    marginTop: 20, 
  },
  bottomImage: {
    position: 'absolute',
    bottom: -125,
  
  },
  image:{
    width: 100,  
    height: 100, 
    resizeMode: 'contain',
    alignItems: 'center',
    marginLeft: 100,
    marginBottom: 10,

    borderRadius:60
  }
});
