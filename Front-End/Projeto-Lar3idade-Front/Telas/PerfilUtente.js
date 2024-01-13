import React, { useState, useEffect } from 'react';
import { StyleSheet, Image, Text, View, TouchableOpacity, FlatList } from 'react-native';

export default function PerfilUtente({ route, navigation }) {
  const [utenteData, setUtenteData] = useState([]);

  useEffect(() => {
    const utenteDataFromParams = route.params ? route.params.utenteData : null;
    if (utenteDataFromParams) {
      setUtenteData([utenteDataFromParams]);
    } else {
      console.error('Dados do utente logado não disponíveis.');
    }
  }, [route.params?.utenteData]);

  // Certifique-se de que utenteData está definido antes de renderizar o componente
  if (!utenteData || utenteData.length === 0) {
    return (
      <View style={styles.container}>
        <Text>Carregando...</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <View style={styles.Imag1}></View>
      <View style={styles.Imag}>
        <View style={styles.utenteInfoContainer}>
          <Text style={styles.heading}>Perfil do Utente</Text>
          <FlatList
            data={utenteData}
            keyExtractor={(item) => item.idUtente.toString()}
            renderItem={({ item }) => (
              <View style={styles.utenteItem}>
                <Text>{`Nome: ${item.nome}`}</Text>
                <Text>{`Email: ${item.email}`}</Text>
              </View>
            )}
          />
        </View>
        <Image source={require('../Image/Image2.png')} style={styles.Image2} />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({

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
    padding:350,
    
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
 
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    padding: 16,
  },
  heading: {
    fontSize: 24,
    textAlign: 'center',
  },
  utenteInfoContainer: {
    marginTop: -300, 
  },
  utenteItem: {
    marginBottom: 16,
    borderColor: '#ccc',
    borderWidth: 1,
    padding: 16,
    borderRadius: 8,
  },
});