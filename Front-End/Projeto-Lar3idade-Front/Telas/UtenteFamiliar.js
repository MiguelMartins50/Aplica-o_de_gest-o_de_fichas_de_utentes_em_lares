import React from 'react';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Image, Text, View, TouchableOpacity, ImageBackground } from 'react-native';

export default function Visitas({ navigation }) {
    const handleSair = () => {
  
        navigation.navigate('Login');
      };
  return (
      <View style={styles.container}>
        <View style={styles.Imag1}>
          <ImageBackground source={require('../Image/Image1.png')} style={styles.Image1}>
          <TouchableOpacity style={styles.sairButton}onPress={handleSair}>
              <Text style={styles.sairButtonText}>Sair</Text>
            </TouchableOpacity>
            
          </ImageBackground>
        </View>

        <Text tyle={styles.ButtonText}>Imformações Utentes</Text>
        
        
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
    marginTop:-175
    
    
  },
  Image2: {
    height:205,
    width:410,
    padding:20,
    marginBottom:-100
   
  },
  Imag:{
    padding:50
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
  ButtonText: {
    color: '#fff',
    fontSize: 17,
    
  },
});