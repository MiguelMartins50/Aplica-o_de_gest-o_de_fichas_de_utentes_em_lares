import React, { useState, useEffect } from 'react';
import { StyleSheet, ImageBackground, Text, View, ScrollView } from 'react-native';

export default function PerfilUtente({ route, navigation }) {
  const { params } = route;
  const { utenteData, utenteNome } = params || {};
  const numeroCC = utenteData?.numero_cc || {};
  const dataValidade = utenteData?.data_validade || {};
  const nif = utenteData?.nif || {};
  const niss = utenteData?.niss || {};
  const nUtenteSaude = utenteData?.n_utenteSaude || {};
  const dataNascimmento = utenteData?.data_nascimento || {};
  const idade = utenteData?.idade || {};
  const estadoCivil = utenteData?.estado_civil || {};
  const morada = utenteData?.morada || {};
  const localidade = utenteData?.localidade || {};
  const codPostal = utenteData?.cod_postal || {};
  const telCasa = utenteData?.telefone_casa || {};
  const tel = utenteData?.telemovel || {};
  const email = utenteData?.email || {};

  useEffect(() => {
    console.log('PerfilUtente - Route Params:', route.params);
  }, [route.params]);

  return (
    <View style={styles.container}>
      {utenteNome ? (
        <ScrollView style={styles.PerfilUtente1}>
          <Text style={styles.TEXTO}>Nome: {utenteNome}</Text>
          <Text style={styles.TEXTO}>Número de Cartão Cidadão: {numeroCC}</Text>
          <Text style={styles.TEXTO}>Data de validade: {dataValidade ? new Date(dataValidade).toLocaleDateString() : ''}</Text>
          <Text style={styles.TEXTO}>NIF: {nif}</Text>
          <Text style={styles.TEXTO}>NISS: {niss}</Text>
          <Text style={styles.TEXTO}>Nº Utente de Saúde: {nUtenteSaude}</Text>
          <Text style={styles.TEXTO}>Data nascimento: {dataNascimmento ? new Date(dataNascimmento).toLocaleDateString() : ''}</Text>
          <Text style={styles.TEXTO}>Idade: {idade}</Text>
          <Text style={styles.TEXTO}>Estado Civil: {estadoCivil}</Text>
          <Text style={styles.TEXTO}>Morada: {morada}</Text>
          <Text style={styles.TEXTO}>Localidade: {localidade}</Text>
          <Text style={styles.TEXTO}>Codigo Postal: {codPostal}</Text>
          <Text style={styles.TEXTO}>Telefone casa: {telCasa}</Text>
          <Text style={styles.TEXTO}>Telemóvel: {tel}</Text>
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
 
  bottomImage: {
    position: 'absolute',
    bottom: -125,
  
  },
});
